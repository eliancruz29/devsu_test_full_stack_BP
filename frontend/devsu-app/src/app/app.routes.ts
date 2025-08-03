import { Routes } from '@angular/router';
import { ClientListComponent } from './clients/pages/client-list/client-list.component';
import { HomeComponent } from './home/home.component';
import { PageNotFoundComponent } from './shared/pages/page-not-found/page-not-found.component';

export const routes: Routes = [
  { path: '', title: 'Home Page', component: HomeComponent },
  { path: 'clients', title: 'Client Page', component: ClientListComponent },
  { path: '**', component: PageNotFoundComponent },  // Wildcard route for a 404 page
];
