import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { Login } from './login/login.model';


@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router) { }

    canActivate() {
        if (localStorage.getItem('currentUser')) {

            let loginInfo: Login = JSON.parse(localStorage.getItem('currentUser') || '{}');
            // logged in so return true
            return loginInfo.token ? true : false;
        }

        // not logged in so redirect to login page
        this.router.navigate(['/login']);
        return false;
    }
}