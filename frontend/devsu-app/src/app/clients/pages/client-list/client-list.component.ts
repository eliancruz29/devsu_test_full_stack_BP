import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClientService } from '../../services/client.service';
import { ClientResponse } from '../../models/client.response';

@Component({
  selector: 'app-client-list',
  imports: [CommonModule, FormsModule],
  providers: [ClientService],
  templateUrl: './client-list.component.html',
  styleUrl: './client-list.component.scss',
})
export class ClientListComponent implements OnInit {
  clients: ClientResponse[] = [];
  loading = false;
  error = '';

  constructor(
    private clientService: ClientService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadClients();
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
      },
    });
  }

  searchByName(event: KeyboardEvent): void {
    if (event.key === 'Enter') {
      this.loading = true;
      const searchTerm = (event.target as HTMLInputElement).value.trim();
      this.clientService.searchByName(searchTerm).subscribe({
        next: (data) => {
          this.clients = data;
          this.loading = false;
        },
        error: (err) => {
          this.error = 'Errora al buscar los clientes.';
          this.loading = false;
        },
      });
    }
  }

  editClient(id: string): void {
    this.router.navigate(['/clientes/editar', id]);
  }

  deleteClient(id: string): void {
    if (confirm('Estas seguro que quiere borrar este Cliente?')) {
      this.clientService.delete(id).subscribe(() => {
        this.loadClients();
      });
    }
  }

  createNew(): void {
    this.router.navigate(['/clientes/crear']);
  }
}
