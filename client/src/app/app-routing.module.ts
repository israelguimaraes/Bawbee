import { AddEntryComponent } from './entries/add-entry/add-entry.component';
import { ListEntryComponent } from './entries/list-entry/list-entry.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';

const routes: Routes = [
    { path: '', redirectTo: '/auth/login', pathMatch: 'full' },
    {
        path: 'auth', component: AuthComponent,
        children: [
            { path: 'login', component: LoginComponent },
            { path: 'register', component: RegisterComponent },
            { path: 'logout', redirectTo: 'login' },
        ]
    },
    { path: 'dashboard', component: DashboardComponent },
    { path: 'entries', component: ListEntryComponent },
    { path: 'entries/add', component: AddEntryComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
