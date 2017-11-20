import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'veiculos',
    templateUrl: './veiculos.component.html'
})
export class VeiculosComponent {
    public veiculos: Veiculo[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/Veiculo').subscribe(result => {
            this.veiculos = result.json() as Veiculo[];
        }, error => console.error(error));
    }
}


