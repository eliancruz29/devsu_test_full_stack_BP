import { Routes } from '@angular/router';
import { ClientListComponent } from './clients/pages/client-list/client-list.component';
import { HomeComponent } from './home/home.component';
import { PageNotFoundComponent } from './shared/pages/page-not-found/page-not-found.component';
import { ClientFormComponent } from './clients/pages/client-form/client-form.component';
import { AccountListComponent } from './accounts/pages/account-list/account-list.component';

export const routes: Routes = [
  { path: '', title: 'Home Page', component: HomeComponent },
  
  { path: 'clients', title: 'Client Page', component: ClientListComponent },
  { path: 'clients/new', component: ClientFormComponent },
  { path: 'clients/edit/:id', component: ClientFormComponent },

  { path: 'accounts', title: 'Account Page', component: AccountListComponent },

  { path: '**', component: PageNotFoundComponent },  // Wildcard route for a 404 page
];
