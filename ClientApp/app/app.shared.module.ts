import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, ActivatedRoute, Router, Routes } from '@angular/router';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {ToastModule} from 'ng2-toastr/ng2-toastr';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './home/navmenu/navmenu.component';
import { HomeComponent } from './home/home.component';
import { VeiculosComponent } from './home/veiculos/veiculos.component';
import { ColaboradoresComponent } from './home/colaboradores/colaboradores.component';
import { LoginComponent } from './login/login.component';
import { Title, BrowserModule } from '@angular/platform-browser';
import { AuthGuard } from './app.auth.guard';
import { LoginService } from './login/login.service';
import { ApontamentosComponent } from './home/apontamentos/apontamentos.component';
import { SelectivePreloadingStrategy } from './SelectivePreloadingStrategy';
import { ColaboradorComponent } from './home/colaboradores/colaborador.component';
import { ColaboradorService } from './home/colaboradores/colaborador.service';


const appRoutes: Routes = [
    { path: 'login', component: LoginComponent, data: { title: 'Login' } },
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', redirectTo: 'home/apontamentos', pathMatch: 'full'},
    {
        path: 'home',
        component: HomeComponent,
        canActivate: [AuthGuard],
        children: [
            { path: '', redirectTo: 'apontamentos', pathMatch: 'full' },
            { path: 'apontamentos', component: ApontamentosComponent, data: { title: 'Apontamentos' } },
            { path: 'colaboradores', component: ColaboradoresComponent,  data: { title: 'Colaboradores' } },
            { path: 'colaborador', component: ColaboradorComponent,  data: { title: 'Colaborador' } },
            { path: 'veiculos', component: VeiculosComponent,  data: { title: 'Veiculos' } },
        ]
    }
];

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        ColaboradoresComponent,
        VeiculosComponent,
        HomeComponent,
        LoginComponent,
        ApontamentosComponent,
        ColaboradorComponent
    ],
    imports: [
        BrowserModule,
        CommonModule,
        HttpModule,
        FormsModule,
        BrowserAnimationsModule,
        ToastModule.forRoot(),
        RouterModule.forRoot(appRoutes)
    ],
    providers: [
        Title,
        AuthGuard,
        LoginService,
        ColaboradorService,
        SelectivePreloadingStrategy
    ],
})
export class AppModuleShared {
}
