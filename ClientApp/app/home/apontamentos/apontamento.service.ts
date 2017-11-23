import { Injectable, Inject } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'
import { BaseService } from '../../shared/BaseService';



@Injectable()
export class ApontamentoService extends BaseService {

    private baseUrl: string = '';

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        super();
        this.baseUrl = baseUrl
    }

    post(model: Apontamento) {

        let body = JSON.stringify(model);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.baseUrl + 'api/apontamento', body, options)
            .map((response: Response) => { }).catch(this.handleError);
    }

    getAll(): Observable<Apontamento[]> {
        return this.http.get(this.baseUrl + 'api/apontamento').map(result => { return result.json() as Apontamento[]; });
    }

    getAllAsCsv(): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'text/csv' });
        let options = new RequestOptions({ headers: headers });
        return this.http.get(this.baseUrl + 'api/apontamento/GetAllAsCsv', options).map(result => { return result; });
    }

    getAllParents(): Observable<Apontamento[]> {
        return this.http.get(this.baseUrl + 'api/apontamento/GetAllParrents').map(result => { return result.json() as Apontamento[]; });
    }

    getAllChilds(id: number): Observable<Apontamento[]> {
        return this.http.get(this.baseUrl + 'api/apontamento/GetAllChilds?id=' + id).map(result => { return result.json() as Apontamento[]; });
    }


}