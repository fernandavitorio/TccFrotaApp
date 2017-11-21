
import { Component, Inject, OnInit, ViewContainerRef } from '@angular/core';
import { Http } from '@angular/http';
import { ColaboradorService } from './colaborador.service';
import { ToastsManager } from 'ng2-toastr';
import { NavigationExtras, Router } from '@angular/router';

@Component({
    selector: 'colaboradores',
    templateUrl: './colaboradores.component.html'
})
export class ColaboradoresComponent implements OnInit {
    public colaboradores: Colaborador[];
    public loading: boolean = false;

    constructor(
        private router: Router,
        public toastr: ToastsManager,
        vcr: ViewContainerRef,
        private colaboradorService: ColaboradorService) {

        this.toastr.setRootViewContainerRef(vcr);
    }

    ngOnInit() {

        this.updateTable();

    }

    updateTable() {
        this.colaboradorService.getAll().subscribe(result => this.colaboradores = result);
    }

    delete(id: number) {

        this.colaboradorService.delete(id)
            .subscribe((response) => this.onSucessDelete(response),
            error => this.onErrorDelete(error)
            );
    }

    onSucessDelete(response: any) {
        this.loading = false;
        this.toastr.success('Colaborador deletado com sucesso', 'Ok');
        this.updateTable();
    }

    onErrorDelete(error: any) {
        this.loading = false;
        this.toastr.error(error, 'Erro');

    }

    edit(model: Colaborador)
    {
        let navigationExtras: NavigationExtras = {
            queryParams: {
                "model" : JSON.stringify(model)
            }
        };
        this.router.navigate(['/home/colaborador'], navigationExtras);
    }
}
