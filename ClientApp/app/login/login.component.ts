import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from './login.service';
import { Login } from './login.model';


@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    model: Login = <Login>{};
    loading = false;
    error = '';

    constructor(
        private router: Router,
        private loginService: LoginService) { }

    ngOnInit() {
        // reset login status
        this.loginService.logout();
    }

    login() {
        this.loading = true;
        this.loginService.login(this.model)
            .subscribe(result => {
                
                this.loading = false;

                if (!result) {
                    this.error = 'Ouve um problema na autenticação, tente novamente';
                    return;
                }


                this.router.navigate(['/']);
            },
            error => {
                this.error = error;
                this.loading = false;
            }
            );
    }
}