import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { ClientService } from '../../services/service';
import { CommonModule } from '@angular/common';
import { EnumObject } from '../../../core/enums/enum-object';
import { isErrorResponse } from '../../../core/helpers/interface.helper';
import { getGenderAsEnumObjectOptions } from '../../../core/helpers/enum.helper';

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
      name: ['', Validators.required],
      gender: [null, Validators.required],
      dateOfBirth: [null, Validators.required],
      identification: ['', Validators.required],
      address: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      password: ['', Validators.required],
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
    formValue.gender = +formValue.gender; // Convert to number
    this.loading = true;
    if (this.clientId) {
      this.clientService.update(this.clientId, formValue).subscribe({
        next: () => {
          this.loading = false;
          this.router.navigate(['/clients']);
        },
        error: () => {
          this.error = 'Failed to update client';
          this.loading = false;
        },
      });
    } else {
      this.clientService.create(formValue).subscribe({
        next: () => {
          this.loading = false;
          this.router.navigate(['/clients']);
        },
        error: (err) => {
          this.error = 'Failed to create client';
          console.log(isErrorResponse(err.error), err.error);
          if (isErrorResponse(err.error)) {
            this.error += `: ${err.error.message}`;
          }
          this.loading = false;
        },
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/clients']);
  }
}

