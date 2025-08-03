import { Routes } from '@angular/router';
import { ClientListComponent } from './modules/clients/client-list.component';

export const routes: Routes = [
  { path: 'clients', title: 'Client Page', component: ClientListComponent }
];
