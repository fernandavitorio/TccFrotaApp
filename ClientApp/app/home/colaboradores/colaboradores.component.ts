
import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'colaboradores',
    templateUrl: './colaboradores.component.html'
})
export class ColaboradoresComponent {
    public colaboradores: Colaborador[];
    
        constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
            http.get(baseUrl + 'api/Colaborador').subscribe(result => {
                this.colaboradores = result.json() as Colaborador[];
            }, error => console.error(error));
        }
}
