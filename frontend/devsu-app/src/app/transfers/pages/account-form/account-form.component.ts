import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../services/transfer.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { areAllPropertiesFulfilled, isErrorResponse } from '../../../core/helpers/interface.helper';
import { getAccountTypeAsEnumObjectOptions, getGenderAsEnumObjectOptions } from '../../../core/helpers/enum.helper';
import { EnumObject } from '../../../core/enums/enum-object';
import { ClientForAccountResponse } from '../../models/account-for-transfer.response';
import { ClientService } from '../../../clients/services/client.service';

@Component({
  selector: 'app-account-form',
  imports: [CommonModule, ReactiveFormsModule],
  providers: [ClientService, AccountService],
  templateUrl: './account-form.component.html',
  styleUrl: './account-form.component.scss'
})
export class AccountFormComponent implements OnInit {
  accountForm!: FormGroup;
  accountId?: string;
  loading = false;
  error = '';
  accountTypes!: EnumObject<number>[];
  clients: ClientForAccountResponse[] = [];

  constructor(
    private fb: FormBuilder,
    private clientService: ClientService,
    private accountService: AccountService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadClients();

    this.accountId = this.route.snapshot.paramMap.get('id') || undefined;

    this.accountTypes = getAccountTypeAsEnumObjectOptions()

    this.accountForm = this.fb.group({
      clientId: this.accountId ? [''] : ['', Validators.required],
      accountNumber: this.accountId ? [''] : ['', Validators.required],
      type: this.accountId ? [null] : [null, Validators.required],
      openingBalance: this.accountId ? [0] : [0, [Validators.required, Validators.min(1)]],
    });

    if (this.accountId) {
      this.loadAccount(this.accountId);
    }
  }

  loadClients(): void {
    this.loading = true;
    this.clientService.getAll().subscribe({
      next: (data) => {
        this.clients = data;
      },
      error: (err) => {
        this.error = `Failed to load clients.`;
      },
      complete: () => {
        this.loading = false;
      },
    });
  }

  loadAccount(id: string): void {
    this.loading = true;
    this.accountService.getById(id).subscribe({
      next: (account) => {
        this.accountForm.patchValue({
          clientId: account.clientId,
          accountNumber: account.accountNumber,
          type: account.type,
          openingBalance: account.openingBalance
        });
        this.loading = false;
      },
      error: () => {
        this.error = 'Failed to load account';
        this.loading = false;
      },
    });
  }

  onSubmit(): void {
    if (this.accountForm.invalid) return;

    const formValue = this.accountForm.value;
    formValue.type = +formValue.type; // Convert to number
    this.loading = true;

    const handleSuccess = () => {
      this.router.navigate(['/cuentas']);
    };

    const handleError = (err: any) => {
      this.error = 'Failed to create account';
      console.log(isErrorResponse(err.error), err.error);
      if (isErrorResponse(err.error)) {
        this.error += `: ${err.error.message}`;
      }
    };

    if (this.accountId) {
      formValue.id = this.accountId; // Add id for update
      const handlers = { next: handleSuccess, error: handleError }

      if (areAllPropertiesFulfilled(formValue)) {
        this.accountService.update(this.accountId, formValue).subscribe(handlers);
      } else {
        this.accountService.partialUpdate(this.accountId, formValue).subscribe(handlers);
      }
    } else {
      this.accountService.create(formValue).subscribe({
        next: handleSuccess,
        error: handleError,
        complete: () => {
          this.loading = false;
        }
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/cuentas']);
  }
}
