import { Component } from '@angular/core';
import { TransferService } from '../../services/transfer.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { TransferResponse } from '../../models/transfer.response';
import { AccountService } from '../../../accounts/services/account.service';
import { AccountForTransferResponse } from '../../models/account-for-transfer.response';
import { DevsuAppConstants } from '../../../shared/constants';

@Component({
  selector: 'app-transfer-list',
  imports: [CommonModule, FormsModule],
  providers: [AccountService, TransferService],
  templateUrl: './transfer-list.component.html',
  styleUrl: './transfer-list.component.scss'
})
export class TransferListComponent {
  accounts: AccountForTransferResponse[] = [];
  transfers: TransferResponse[] = [];
  loading = false;
  error = '';
  constants = DevsuAppConstants;

  constructor(
    private accountService: AccountService,
    private transferService: TransferService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadAccounts();
    this.loadTransfers();
  }

  loadAccounts(): void {
    this.accountService.getAll().subscribe({
      next: (data) => {
        this.accounts = data;
        this.transfers = this.mapAccountNameForTransfers(this.transfers, this.accounts); // this could be done in a better way but for simplicity, we do it in this way
      },
      error: (err) => {
        this.error = `Error al cargar las cuentas.`;
      },
    });
  }

  loadTransfers(accountId?: string): void {
    this.loading = true;
    this.transferService.getAll(accountId).subscribe({
      next: (data) => {
        this.transfers = this.mapAccountNameForTransfers(data, this.accounts); // this could be done in a better way but for simplicity, we do it in this way
        this.loading = false;
      },
      error: (err) => {
        this.error = `Error al cargar los movimientos.`;
        this.loading = false;
      },
    });
  }

  mapAccountNameForTransfers(transfers: TransferResponse[], accounts: AccountForTransferResponse[]): TransferResponse[] {
    return transfers.map(transfer => {
      const account = accounts.find(a => a.id === transfer.accountId);
      return {
        ...transfer,
        accountNumber: account ? account.accountNumber : 'Cuenta desconocida'
      };
    });
  }

  onSelectChange(event: Event) {
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.loadTransfers(selectedValue);
  }

  editTransfer(id: string): void {
    this.router.navigate(['/movimientos/editar', id]);
  }

  deleteTransfer(id: string): void {
    if (confirm('Estas seguro que quiere borrar este Movimiento?')) {
      this.transferService.delete(id).subscribe(() => {
        this.loadTransfers();
      });
    }
  }

  createNew(): void {
    this.router.navigate(['/movimientos/crear']);
  }
}
