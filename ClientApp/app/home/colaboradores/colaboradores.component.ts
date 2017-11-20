
import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ColaboradorService } from './colaborador.service';

@Component({
    selector: 'colaboradores',
    templateUrl: './colaboradores.component.html'
})
export class ColaboradoresComponent implements OnInit {
    public colaboradores: Colaborador[];


    constructor(
        private colaboradorService: ColaboradorService) { }

    ngOnInit() {

        this.colaboradorService.getAll().subscribe(result => this.colaboradores = result);

    }
}
