import { Component, Inject, ViewContainerRef } from '@angular/core';
import { Http } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastsManager } from 'ng2-toastr/ng2-toastr';
import { ApontamentoService } from './apontamento.service';
import { ColaboradorService } from '../colaboradores/colaborador.service';
import { VeiculoService } from '../veiculos/veiculo.service';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { Login } from '../../login/login.model';

@Component({
    selector: 'apontamento',
    templateUrl: './apontamento.component.html',
    styleUrls: ['./apontamento.component.css']
})
export class ApontamentoComponent implements OnInit {

    model: Apontamento = <Apontamento>{};
    motoristas: Colaborador[];
    coletores: Colaborador[];
    veiculos: Veiculo[];
    loading: boolean = false;
    currentUser: Colaborador = <Colaborador>{};

    constructor(
        public toastr: ToastsManager,
        vcr: ViewContainerRef,
        private router: Router,
        private apontamentoService: ApontamentoService,
        private colaboradorService: ColaboradorService,
        private veiculoService: VeiculoService) {

        this.toastr.setRootViewContainerRef(vcr);
    }

    ngOnInit() {
        this.refreshMotoristas();
        this.refreshColetores();
        this.refreshVeiculos();
        this.refreshCurrentUser();
    }

    save() {
        this.loading = true;

        this.apontamentoService.post(this.model)
            .subscribe((response) => this.onSucessSave(response),
            (error) => this.onErrorSave(error)
            );

    }

    onSucessSave(response: any) {
        this.loading = false;
        this.router.navigate(['/home/apontamentos']);
    }

    onErrorSave(error: any) {
        this.toastr.error(error);
        this.loading = false;
    }

    refreshMotoristas() {
        this.colaboradorService.getByType('MOTORISTA').subscribe(result => this.motoristas = result);
    }

    refreshColetores() {
        this.colaboradorService.getByType('COLETOR').subscribe(result => this.coletores = result);
    }

    refreshVeiculos() {
        this.veiculoService.getAll().subscribe(result => this.veiculos = result);
    }

    refreshCurrentUser() {
        let loginInfo: Login = JSON.parse(localStorage.getItem('currentUser') || '{}');
        this.currentUser = loginInfo ? loginInfo.colaborator : <Colaborador>{};
    }

}