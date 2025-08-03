import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ClientResponse } from '../../core/models/client.models';
import { ClientService } from '../../core/services/client.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

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
        this.error = 'Failed to load clients.';
        this.loading = false;
      },
    });
  }

  searchByName(event: KeyboardEvent): void {
    if (event.key === 'Enter') {
      console.log('Enter pressed');
    }
  }

  editClient(id: string): void {
    this.router.navigate(['/clients/edit', id]);
  }

  deleteClient(id: string): void {
    if (confirm('Are you sure you want to delete this client?')) {
      this.clientService.delete(id).subscribe(() => {
        this.loadClients();
      });
    }
  }

  createNew(): void {
    this.router.navigate(['/clients/new']);
  }
}
