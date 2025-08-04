import { Component } from '@angular/core';
import { TransferService } from '../../services/transfer.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { TransferResponse } from '../../models/transfer.response';
import { AccountService } from '../../../accounts/services/account.service';
import { AccountForTransferResponse } from '../../models/account-for-transfer.response';

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
      },
      error: (err) => {
        this.error = `Failed to load accounts.`;
      },
      complete: () => {
        this.transfers = this.mapAccountNameForTransfers(this.transfers, this.accounts); // this could be done in a better way but for simplicity, we do it in this way
      },
    });
  }

  loadTransfers(accountId?: string): void {
    this.loading = true;
    this.transferService.getAll(accountId).subscribe({
      next: (data) => {
        this.transfers = this.mapAccountNameForTransfers(data, this.accounts); // this could be done in a better way but for simplicity, we do it in this way
      },
      error: (err) => {
        this.error = `Failed to load transfers.`;
      },
      complete: () => {
        this.loading = false;
      },
    });
  }

  mapAccountNameForTransfers(transfers: TransferResponse[], accounts: AccountForTransferResponse[]): TransferResponse[] {
    return transfers.map(transfer => {
      const account = accounts.find(a => a.id === transfer.accountId);
      return {
        ...transfer,
        accountNumber: account ? account.accountNumber : 'Unknown Account'
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
    if (confirm('Are you sure you want to delete this transfer?')) {
      this.transferService.delete(id).subscribe(() => {
        this.loadTransfers();
      });
    }
  }

  createNew(): void {
    this.router.navigate(['/movimientos/crear']);
  }
}
