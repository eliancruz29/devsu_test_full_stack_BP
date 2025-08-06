import { Component } from '@angular/core';
import { ClientService } from '../../clients/services/client.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { SimpleClientResponse } from '../../clients/models/simple-client.response';
import { TransfersReportResponse } from '../models/transfers-report.response';
import { isErrorResponse } from '../../core/helpers/interface.helper';
import { ReportsService } from '../services/reports.service';
import { CommonModule } from '@angular/common';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';
import { DatePipe, CurrencyPipe } from '@angular/common';
import { DevsuAppConstants } from '../../shared/constants';

@Component({
  selector: 'app-transfers-report',
  imports: [CommonModule, ReactiveFormsModule],
  providers: [ClientService, ReportsService, DatePipe, CurrencyPipe],
  templateUrl: './transfers-report.component.html',
  styleUrl: './transfers-report.component.scss'
})
export class TransfersReportComponent {
  transfersReportForm!: FormGroup;
  loading = false;
  error = '';
  clients: SimpleClientResponse[] = [];
  transfersReportData: TransfersReportResponse[] = [];
  constants = DevsuAppConstants;

  constructor(
    private fb: FormBuilder,
    private clientService: ClientService,
    private reportsService: ReportsService,
    private datePipe: DatePipe,
    private currencyPipe: CurrencyPipe
  ) { }

  ngOnInit(): void {
    this.loadClients();

    this.transfersReportForm = this.fb.group({
      clientId: ['', Validators.required],
      startDate: [null, Validators.required],
      endDate: [null, Validators.required],
    });
  }

  loadClients(): void {
    this.loading = true;
    this.clientService.getAll().subscribe({
      next: (data) => {
        this.clients = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = `Error al cargar los clientes.`;
        this.loading = false;
      }
    });
  }

  onSubmit(): void {
    if (this.transfersReportForm.invalid) return;

    const formValue = this.transfersReportForm.value;
    this.loading = true;

    const handleSuccess = (response: TransfersReportResponse[]) => {
      console.log(response);
      this.transfersReportData = response;
      this.loading = false;
      if (this.transfersReportData.length > 0) {
        this.exportPDF();
      } else {
        this.error = 'Este cliente no tiene movimientos.';
      }
    };

    const handleError = (err: any) => {
      this.error = 'Error al tratar de exporta los movimientos.';
      if (isErrorResponse(err.error)) {
        this.error += `: ${err.error.message}`;
      }
      this.loading = false;
    };

    this.reportsService.getTransfersReport(formValue.clientId, formValue.startDate, formValue.endDate)
      .subscribe({ next: handleSuccess, error: handleError });
  }

  exportPDF(): void {
    const doc = new jsPDF();
    const rows = this.transfersReportData.map(r => [
      r.clientName,
      this.datePipe.transform(r.date, this.constants.DATE_FORMAT),
      r.accountNumber,
      r.typeName,
      this.currencyPipe.transform(r.openingBalance, this.constants.DEFAULT_CURRENCY, 'symbol', this.constants.CURRENCY_DECIMALS),
      this.currencyPipe.transform(r.amount, this.constants.DEFAULT_CURRENCY, 'symbol', this.constants.CURRENCY_DECIMALS),
      this.currencyPipe.transform(r.balance, this.constants.DEFAULT_CURRENCY, 'symbol', this.constants.CURRENCY_DECIMALS),
      r.statusName
    ]);

    autoTable(doc, {
      head: [['Cliente', 'Fecha', 'Numero Cuenta', 'Tipo', 'Saldo Inicial', 'Movimiento', 'Saldo Disponible', 'Estado']],
      body: rows
    });

    doc.save(`Reporte de movimientos - ${this.transfersReportData[0].clientName}.pdf`);
  }
}
