import { Component, ViewContainerRef } from '@angular/core';
import { ApontamentoService } from './apontamento.service';
import { Router } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';

@Component({
    selector: 'apontamentos',
    templateUrl: './apontamentos.component.html',
    styleUrls: ['./apontamentos.component.css']
})
export class ApontamentosComponent {
    public apontamentosPai: Apontamento[];
    public loading: boolean = false;

    constructor(
        private router: Router,
        public toastr: ToastsManager,
        vcr: ViewContainerRef,
        private apontamentoService: ApontamentoService) {

        this.toastr.setRootViewContainerRef(vcr);
    }

    ngOnInit() {

        this.updateTable();

    }

    updateTable() {
        this.apontamentoService.getAllParents().subscribe(result => this.apontamentosPai = result);
    }

    export() {

    }

}
