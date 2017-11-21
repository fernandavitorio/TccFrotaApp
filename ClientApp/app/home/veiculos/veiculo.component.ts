import { Component, Inject, ViewContainerRef } from '@angular/core';
import { Http } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastsManager } from 'ng2-toastr/ng2-toastr';
import { VeiculoService } from './veiculo.service';

@Component({
    selector: 'veiculo',
    templateUrl: './veiculo.component.html',
    styleUrls: ['./veiculo.component.css']
})
export class VeiculoComponent {
    model: Veiculo = <Veiculo>{};
    loading: boolean = false;
    isEditing: boolean = false;
    pageH1: string = "Cadastro de Veiculo";


    constructor(
        public toastr: ToastsManager,
        vcr: ViewContainerRef,
        private router: Router,
        private activeRoute: ActivatedRoute,
        private veiculoService: VeiculoService) {

        this.toastr.setRootViewContainerRef(vcr);

        this.activeRoute.queryParams.subscribe(params => {
            this.model = JSON.parse(params["model"] || '{}');
            this.isEditing = this.model.id && this.model.id > 0;
            this.pageH1 = this.isEditing ? "Edição de Veiculo" : "Cadastro de Veiculo";
        });
    }

    save() {
        this.loading = true;

        if (this.model.id && this.model.id != 0) {

            this.veiculoService.put(this.model)
                .subscribe((response) => this.onSucessSave(response),
                error => this.onErrorSave(error)
                );

            return;
        }

        this.veiculoService.post(this.model)
            .subscribe((response) => this.onSucessSave(response),
            (error) => this.onErrorSave(error)
            );

    }

    onSucessSave(response: any) {
        this.loading = false;
        this.router.navigate(['/home/veiculos']);
    }

    onErrorSave(error: any) {
        this.toastr.error(error);
        this.loading = false;
    }

}