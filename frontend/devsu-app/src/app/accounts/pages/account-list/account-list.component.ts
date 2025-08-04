import { Component } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AccountResponse } from '../../models/account.response';

@Component({
  selector: 'app-account-list',
  imports: [CommonModule, FormsModule],
  providers: [AccountService],
  templateUrl: './account-list.component.html',
  styleUrl: './account-list.component.scss'
})
export class AccountListComponent {
  accounts: AccountResponse[] = [];
  loading = false;
  error = '';

  constructor(
    private accountService: AccountService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadAccounts();
  }

  loadAccounts(): void {
    this.loading = true;
    this.accountService.getAll().subscribe({
      next: (data) => {
        this.accounts = data;
      },
      error: (err) => {
        this.error = `Failed to load accounts by ${this.searchByName} term.`;
      },
      complete: () => {
        this.loading = false;
      },
    });
  }

  searchByName(event: KeyboardEvent): void {
    if (event.key === 'Enter') {
      this.loading = true;
      const searchTerm = (event.target as HTMLInputElement).value.trim();
      this.accountService.searchByName(searchTerm).subscribe({
        next: (data) => {
          this.accounts = data;
          this.loading = false;
        },
        error: (err) => {
          this.error = 'Failed to search accounts.';
          this.loading = false;
        },
      });
    }
  }

  editAccount(id: string): void {
    this.router.navigate(['/accounts/edit', id]);
  }

  deleteAccount(id: string): void {
    if (confirm('Are you sure you want to delete this account?')) {
      this.accountService.delete(id).subscribe(() => {
        this.loadAccounts();
      });
    }
  }

  createNew(): void {
    this.router.navigate(['/accounts/new']);
  }
}
