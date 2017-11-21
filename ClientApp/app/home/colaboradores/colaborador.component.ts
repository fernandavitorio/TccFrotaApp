
import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';
import { ColaboradorService } from './colaborador.service';

@Component({
    selector: 'colaborador',
    templateUrl: './colaborador.component.html',
    styleUrls: ['./colaborador.component.css']
})
export class ColaboradorComponent {
    model: Colaborador = <Colaborador>{};
    loading = false;
    error = '';
    withLogin = false;

    constructor(
        private router: Router,
        private colaboradorService: ColaboradorService) {

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
            error => this.onErrorSave(error)
            );

    }

    onSucessSave(response: any) {
        this.loading = false;
        this.router.navigate(['/home/colaboradores']);
    }

    onErrorSave(error: any) {
        this.error = error;
        this.loading = false;
    }

    onRoleChange(newValue: string) {
        this.withLogin = newValue != 'COLETOR';
    }
}
