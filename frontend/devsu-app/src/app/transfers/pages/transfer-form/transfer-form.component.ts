import { Component, OnInit } from '@angular/core';
import { TransferService } from '../../services/transfer.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { areAllPropertiesFulfilled, isErrorResponse } from '../../../core/helpers/interface.helper';
import { getTransferTypeAsEnumObjectOptions } from '../../../core/helpers/enum.helper';
import { EnumObject } from '../../../core/enums/enum-object';
import { AccountForTransferResponse } from '../../models/account-for-transfer.response';
import { AccountService } from '../../../accounts/services/account.service';
import { isNullOrUndefinedOrEmpty } from '../../../core/helpers/string.helpers';

@Component({
  selector: 'app-transfer-form',
  imports: [CommonModule, ReactiveFormsModule],
  providers: [AccountService, TransferService],
  templateUrl: './transfer-form.component.html',
  styleUrl: './transfer-form.component.scss'
})
export class TransferFormComponent implements OnInit {
  transferForm!: FormGroup;
  transferId?: string;
  loading = false;
  error = '';
  transferTypes!: EnumObject<number>[];
  accounts: AccountForTransferResponse[] = [];

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private transferService: TransferService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadAccounts();

    this.transferId = this.route.snapshot.paramMap.get('id') || undefined;

    this.transferTypes = getTransferTypeAsEnumObjectOptions()

    this.transferForm = this.fb.group({
      accountId: this.transferId ? [''] : ['', Validators.required],
      type: this.transferId ? [null] : [null, Validators.required],
      amount: this.transferId ? [0] : [0, [Validators.required, Validators.min(1)]],
    });

    if (this.transferId) {
      this.loadTransfer(this.transferId);
    }
  }

  loadAccounts(): void {
    this.loading = true;
    this.accountService.getAll().subscribe({
      next: (data) => {
        this.accounts = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = `Failed to load accounts.`;
        this.loading = false;
      },
    });
  }

  loadTransfer(id: string): void {
    this.loading = true;
    this.transferService.getById(id).subscribe({
      next: (transfer) => {
        this.transferForm.patchValue({
          accountId: transfer.accountId,
          type: transfer.type,
          amount: transfer.amount
        });
        this.loading = false;
      },
      error: () => {
        this.error = 'Failed to load transfer';
        this.loading = false;
      },
    });
  }

  onSubmit(): void {
    if (this.transferForm.invalid) return;

    const formValue = this.transferForm.value;
    this.loading = true;

    const handleSuccess = () => {
      this.router.navigate(['/movimientos']);
      this.loading = false;
    };

    const handleError = (err: any) => {
      this.error = 'Failed to create transfer';
      if (isErrorResponse(err.error)) {
        this.error += `: ${err.error.message}`;
      }
      this.loading = false;
    };

    const propertiesFulfilled = areAllPropertiesFulfilled(formValue);

    if (isNullOrUndefinedOrEmpty(formValue.type)) {
      delete formValue.type; // Remove type if not selected
    } else {
      formValue.type = +formValue.type; // Convert to number
    }

    if (this.transferId) {
      formValue.id = this.transferId; // Add id for update
      const handlers = { next: handleSuccess, error: handleError }

      if (propertiesFulfilled) {
        this.transferService.update(this.transferId, formValue).subscribe(handlers);
      } else {
        this.transferService.partialUpdate(this.transferId, formValue).subscribe(handlers);
      }
    } else {
      this.transferService.create(formValue).subscribe({
        next: handleSuccess,
        error: handleError,
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/movimientos']);
  }
}
