import { Component } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AccountResponse } from '../../models/account.response';
import { ClientService } from '../../../clients/services/client.service';
import { ClientForAccountResponse } from '../../models/client-for-account.response';

@Component({
  selector: 'app-account-list',
  imports: [CommonModule, FormsModule],
  providers: [ClientService, AccountService],
  templateUrl: './account-list.component.html',
  styleUrl: './account-list.component.scss'
})
export class AccountListComponent {
  clients: ClientForAccountResponse[] = [];
  loadingClients = false;
  errorClients = '';

  accounts: AccountResponse[] = [];
  loadingAccounts = false;
  errorAccounts = '';

  constructor(
    private clientService: ClientService,
    private accountService: AccountService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadClients();
    this.loadAccounts();
  }

  loadClients(): void {
    this.loadingClients = true;
    this.clientService.getAll().subscribe({
      next: (data) => {
        this.clients = data;
      },
      error: (err) => {
        this.errorClients = `Failed to load clients.`;
      },
      complete: () => {
        this.accounts = this.mapClientNameForAccounts(this.accounts, this.clients); // this could be done in a better way but for simplicity, we do it in this way
        this.loadingClients = false;
      },
    });
  }

  loadAccounts(clientId?: string): void {
    this.loadingAccounts = true;
    this.accountService.getAll(clientId).subscribe({
      next: (data) => {
        this.accounts = this.mapClientNameForAccounts(data, this.clients); // this could be done in a better way but for simplicity, we do it in this way
      },
      error: (err) => {
        this.errorAccounts = `Failed to load accounts.`;
      },
      complete: () => {
        this.loadingAccounts = false;
      },
    });
  }

  mapClientNameForAccounts(accounts: AccountResponse[], clients: ClientForAccountResponse[]): AccountResponse[] {
    return accounts.map(account => {
      const client = clients.find(c => c.id === account.clientId);
      return {
        ...account,
        clientName: client ? client.name : 'Unknown Client'
      };
    });
  }

  onSelectChange(event: Event) {
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.loadAccounts(selectedValue);
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
