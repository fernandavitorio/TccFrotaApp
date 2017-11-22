import { Injectable, Inject } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'
import { BaseService } from '../../shared/BaseService';



@Injectable()
export class ColaboradorService extends BaseService {

    private baseUrl: string = '';

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        super();
        this.baseUrl = baseUrl
    }

    post(model: Colaborador) {

        let body = JSON.stringify(model);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.baseUrl + 'api/colaborador', body, options)
            .map((response: Response) => { }).catch(this.handleError);
    }

    put(model: Colaborador) {

        let body = JSON.stringify(model);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.put(this.baseUrl + 'api/colaborador/' + model.id, body, options)
            .map((response: Response) => { }).catch(this.handleError);
    }

    getAll(): Observable<Colaborador[]> {
        return this.http.get(this.baseUrl + 'api/colaborador').map(result => { return result.json() as Colaborador[]; });
    }

    getByType(funcao: string): Observable<Colaborador[]> {
        return this.http.get(this.baseUrl + 'api/colaborador/getByType/'+funcao).map(result => { return result.json() as Colaborador[]; });
    }

    delete(id: number) {
        return this.http.delete(this.baseUrl + 'api/colaborador/' + id)
            .map((response: Response) => { }).catch(this.handleError);
    }
}