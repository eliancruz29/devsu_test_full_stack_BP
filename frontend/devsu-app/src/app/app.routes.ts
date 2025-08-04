import { Routes } from '@angular/router';
import { ClientListComponent } from './clients/pages/client-list/client-list.component';
import { HomeComponent } from './home/home.component';
import { PageNotFoundComponent } from './shared/pages/page-not-found/page-not-found.component';
import { ClientFormComponent } from './clients/pages/client-form/client-form.component';
import { AccountListComponent } from './accounts/pages/account-list/account-list.component';
import { AccountFormComponent } from './accounts/pages/account-form/account-form.component';

export const routes: Routes = [
  { path: '', title: 'Página Inicio', component: HomeComponent },
  
  { path: 'clientes', title: 'Página Clientes', component: ClientListComponent },
  { path: 'clientes/crear', title: 'Página Crear Cliente', component: ClientFormComponent },
  { path: 'clientes/editar/:id', title: 'Página Editar Cliente', component: ClientFormComponent },

  { path: 'cuentas', title: 'Página Cuentas', component: AccountListComponent },
  { path: 'cuentas/crear', title: 'Página Crear Cuenta', component: AccountFormComponent },

  { path: '**', component: PageNotFoundComponent },  // Wildcard route for a 404 page
];
