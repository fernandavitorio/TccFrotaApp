import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'
import { Login } from './login.model';
import { BaseService } from '../shared/BaseService';

@Injectable()
export class LoginService extends BaseService {
    public loginInfo: Login;

    constructor(private http: Http) {
        super();
        // set token if saved in local storage
        this.loginInfo = JSON.parse(localStorage.getItem('currentUser') || '{}');
    }

    login(login: Login) {
        this.loginInfo = login;

        let body = JSON.stringify({ username: login.email, password: login.password });
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        
        return this.http.post('/api/login', body, options)
            .map((response: Response) => {
                // login successful if there's a jwt token in the response
                let token = response.json() && response.json().token;
                if (token) {
                    // set token property
                    this.loginInfo.token = token;
                    this.loginInfo.name = response.json() && response.json().name
                    // store username and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(this.loginInfo));

                    // return true to indicate successful login
                    return true;
                } else {
                    // return false to indicate failed login
                    return false;
                }
            }).catch( this.handleError);
    }

    logout(): void {
        // clear token remove user from local storage to log user out
        this.loginInfo!.token = '';
        localStorage.setItem('currentUser', JSON.stringify({}));
    }
}