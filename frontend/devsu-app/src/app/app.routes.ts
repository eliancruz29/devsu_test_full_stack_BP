import { Routes } from '@angular/router';
import { ClientListComponent } from './modules/clients/client-list.component';
import { HomeComponent } from './home/home.component';

export const routes: Routes = [
  { path: '', title: 'Home Page', component: HomeComponent },
  { path: 'clients', title: 'Client Page', component: ClientListComponent }
];
