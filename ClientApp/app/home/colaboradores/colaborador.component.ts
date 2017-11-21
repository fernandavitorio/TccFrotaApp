
import { Component, Inject, ViewContainerRef } from '@angular/core';
import { Http } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { ColaboradorService } from './colaborador.service';
import { ToastsManager } from 'ng2-toastr/ng2-toastr';

@Component({
    selector: 'colaborador',
    templateUrl: './colaborador.component.html',
    styleUrls: ['./colaborador.component.css']
})
export class ColaboradorComponent {
    model: Colaborador = <Colaborador>{};
    loading: boolean = false;
    withLogin: boolean = false;
    isEditing: boolean = false;
    pageH1: string = "Cadastro de Colaborador";


    constructor(
        public toastr: ToastsManager,
        vcr: ViewContainerRef,
        private router: Router,
        private activeRoute: ActivatedRoute,
        private colaboradorService: ColaboradorService) {

        this.toastr.setRootViewContainerRef(vcr);

        this.activeRoute.queryParams.subscribe(params => {
            this.model = JSON.parse(params["model"]) || <Colaborador>{};
            this.isEditing = this.model.id && this.model.id > 0;
            this.pageH1 = this.isEditing ? "Edição de Colaborador" : "Cadastro de Colaborador";
            this.onRoleChange(this.model.funcao || 'COLETOR');
        });
    }

    save() {
        this.loading = true;

        if (this.model.id && this.model.id != 0) {

            this.colaboradorService.put(this.model)
                .subscribe((response) => this.onSucessSave(response),
                error => this.onErrorSave(error)
                );

            return;
        }

        this.colaboradorService.post(this.model)
            .subscribe((response) => this.onSucessSave(response),
            (error) => this.onErrorSave(error)
            );

    }

    onSucessSave(response: any) {
        this.loading = false;
        this.router.navigate(['/home/colaboradores']);
    }

    onErrorSave(error: any) {
        this.toastr.error(error, 'Erro');
        this.loading = false;
    }

    onRoleChange(newValue: string) {
        this.withLogin = newValue != 'COLETOR';
    }
}
