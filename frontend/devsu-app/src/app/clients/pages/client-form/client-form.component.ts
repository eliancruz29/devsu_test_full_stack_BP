import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { ClientService } from '../../services/client.service';
import { CommonModule } from '@angular/common';
import { EnumObject } from '../../../core/enums/enum-object';
import { areAllPropertiesFulfilled, isErrorResponse } from '../../../core/helpers/interface.helper';
import { getGenderAsEnumObjectOptions } from '../../../core/helpers/enum.helper';
import { isNullOrUndefinedOrEmpty } from '../../../core/helpers/string.helpers';

@Component({
  selector: 'app-client-form',
  imports: [CommonModule, ReactiveFormsModule],
  providers: [ClientService],
  templateUrl: './client-form.component.html',
  styleUrl: './client-form.component.scss'
})
export class ClientFormComponent implements OnInit {
  clientForm!: FormGroup;
  clientId?: string;
  loading = false;
  error = '';
  genders!: EnumObject<number>[];

  constructor(
    private fb: FormBuilder,
    private clientService: ClientService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.clientId = this.route.snapshot.paramMap.get('id') || undefined;

    this.genders = getGenderAsEnumObjectOptions()

    this.clientForm = this.fb.group({
      name: this.clientId ? [''] : ['', Validators.required],
      gender: this.clientId ? [null] : [null, Validators.required],
      dateOfBirth: this.clientId ? [null] : [null, Validators.required],
      identification: this.clientId ? [''] : ['', Validators.required],
      address: this.clientId ? [''] : ['', Validators.required],
      phoneNumber: this.clientId ? [''] : ['', Validators.required],
      password: this.clientId ? [''] : ['', Validators.required],
    });

    if (this.clientId) {
      this.loadClient(this.clientId);
    }
  }

  loadClient(id: string): void {
    this.loading = true;
    this.clientService.getById(id).subscribe({
      next: (client) => {
        this.clientForm.patchValue({
          name: client.name,
          gender: client.gender,
          dateOfBirth: client.dateOfBirth.split('T')[0], // just date
          identification: client.identification,
          address: client.address,
          phoneNumber: client.phoneNumber,
          password: client.password,
        });
        this.loading = false;
      },
      error: () => {
        this.error = 'Failed to load client';
        this.loading = false;
      },
    });
  }

  onSubmit(): void {
    if (this.clientForm.invalid) return;

    const formValue = this.clientForm.value;
    this.loading = true;

    const handleSuccess = () => {
      this.router.navigate(['/clientes']);
      this.loading = false;
    };

    const handleError = (err: any) => {
      this.error = 'Failed to create client';
      if (isErrorResponse(err.error)) {
        this.error += `: ${err.error.message}`;
      }
      this.loading = false;
    };

    const propertiesFulfilled = areAllPropertiesFulfilled(formValue);

    if (isNullOrUndefinedOrEmpty(formValue.gender)) {
      delete formValue.gender; // Remove gender if not selected
    } else {
      formValue.gender = +formValue.gender; // Convert to number
    }

    if (this.clientId) {
      formValue.id = this.clientId; // Add id for update
      const handlers = { next: handleSuccess, error: handleError }

      if (propertiesFulfilled) {
        this.clientService.update(this.clientId, formValue).subscribe(handlers);
      } else {
        this.clientService.partialUpdate(this.clientId, formValue).subscribe(handlers);
      }
    } else {
      this.clientService.create(formValue).subscribe({
        next: handleSuccess,
        error: handleError,
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/clientes']);
  }
}

