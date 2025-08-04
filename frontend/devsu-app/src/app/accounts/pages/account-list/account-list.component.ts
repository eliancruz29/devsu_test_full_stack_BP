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
  accounts: AccountResponse[] = [];
  loading = false;
  error = '';

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
    this.clientService.getAll().subscribe({
      next: (data) => {
        this.clients = data;
        this.accounts = this.mapClientNameForAccounts(this.accounts, this.clients); // this could be done in a better way but for simplicity, we do it in this way
      },
      error: (err) => {
        this.error = `Error al cargar los clientes.`;
      },
    });
  }

  loadAccounts(clientId?: string): void {
    this.loading = true;
    this.accountService.getAll(clientId).subscribe({
      next: (data) => {
        this.accounts = this.mapClientNameForAccounts(data, this.clients); // this could be done in a better way but for simplicity, we do it in this way
        this.loading = false;
      },
      error: (err) => {
        this.error = `Error al cargar las cuentas.`;
        this.loading = false;
      },
    });
  }

  mapClientNameForAccounts(accounts: AccountResponse[], clients: ClientForAccountResponse[]): AccountResponse[] {
    return accounts.map(account => {
      const client = clients.find(c => c.id === account.clientId);
      return {
        ...account,
        clientName: client ? client.name : 'Cliente desconocido'
      };
    });
  }

  onSelectChange(event: Event) {
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.loadAccounts(selectedValue);
  }

  editAccount(id: string): void {
    this.router.navigate(['/cuentas/editar', id]);
  }

  deleteAccount(id: string): void {
    if (confirm('Estas seguro que quiere borrar esta Cuenta?')) {
      this.accountService.delete(id).subscribe(() => {
        this.loadAccounts();
      });
    }
  }

  createNew(): void {
    this.router.navigate(['/cuentas/crear']);
  }
}
