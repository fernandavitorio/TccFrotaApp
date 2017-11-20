import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, ActivatedRoute, Router, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './home/navmenu/navmenu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './home/fetchdata/fetchdata.component';
import { CounterComponent } from './home/counter/counter.component';
import { LoginComponent } from './login/login.component';
import { Title, BrowserModule } from '@angular/platform-browser';
import { AuthGuard } from './app.auth.guard';
import { LoginService } from './login/login.service';
import { ApontamentosComponent } from './home/apontamentos/apontamentos.component';


const appRoutes: Routes = [
    { path: 'login', component: LoginComponent, data: { title: 'Login' } },
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: '**', redirectTo: 'home' },
    {
        path: 'home',
        component: HomeComponent,
        canActivate: [AuthGuard],
        children: [
            { path: '', component: ApontamentosComponent },
            { path: 'apontamentos', component: ApontamentosComponent, outlet: 'home', data: { title: 'Apontamentos' } },
            { path: 'counter', component: CounterComponent, outlet: 'home', data: { title: 'Counter' } },
            { path: 'fetch-data', component: FetchDataComponent, outlet: 'home', data: { title: 'Fetch' } },
        ]
    }
];

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        LoginComponent,
        ApontamentosComponent
    ],
    imports: [
        BrowserModule,
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot(appRoutes)
    ],
    providers: [
        Title,
        AuthGuard,
        LoginService,
    ],
})
export class AppModuleShared {
}
