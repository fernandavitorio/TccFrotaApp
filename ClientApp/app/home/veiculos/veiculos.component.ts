import { Component, Inject, OnInit, ViewContainerRef } from '@angular/core';
import { Http } from '@angular/http';
import { ToastsManager } from 'ng2-toastr';
import { NavigationExtras, Router } from '@angular/router';
import { VeiculoService } from './veiculo.service';

@Component({
    selector: 'veiculos',
    templateUrl: './veiculos.component.html'
})
export class VeiculosComponent {
    public veiculos: Veiculo[];
    public loading: boolean = false;

    constructor(
        private router: Router,
        public toastr: ToastsManager,
        vcr: ViewContainerRef,
        private veiculoService: VeiculoService) {

        this.toastr.setRootViewContainerRef(vcr);
    }

    ngOnInit() {

        this.updateTable();

    }

    updateTable() {
        this.veiculoService.getAll().subscribe(result => this.veiculos = result);
    }

    delete(id: number) {

        this.veiculoService.delete(id)
            .subscribe((response) => this.onSucessDelete(response),
            error => this.onErrorDelete(error)
            );
    }

    onSucessDelete(response: any) {
        this.loading = false;
        this.toastr.success('Veiculo deletado com sucesso');
        this.updateTable();
    }

    onErrorDelete(error: any) {
        this.loading = false;
        this.toastr.error(error);

    }

    edit(model: Veiculo) {
        let navigationExtras: NavigationExtras = {
            queryParams: {
                "model": JSON.stringify(model)
            }
        };
        this.router.navigate(['/home/veiculo'], navigationExtras);
    }
}


