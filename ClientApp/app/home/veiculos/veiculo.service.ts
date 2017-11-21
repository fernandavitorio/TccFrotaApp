import { Injectable, Inject } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'
import { BaseService } from '../../shared/BaseService';



@Injectable()
export class VeiculoService extends BaseService {

    private baseUrl: string = '';

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        super();
        this.baseUrl = baseUrl
    }

    post(model: Veiculo) {

        let body = JSON.stringify(model);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.baseUrl + 'api/veiculo', body, options)
            .map((response: Response) => { }).catch(this.handleError);
    }

    put(model: Veiculo) {

        let body = JSON.stringify(model);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.put(this.baseUrl + 'api/veiculo/' + model.id, body, options)
            .map((response: Response) => { }).catch(this.handleError);
    }

    getAll(): Observable<Veiculo[]> {
        return this.http.get(this.baseUrl + 'api/veiculo').map(result => { return result.json() as Veiculo[]; });
    }

    delete(id: number) {
        return this.http.delete(this.baseUrl + 'api/veiculo/' + id)
            .map((response: Response) => { }).catch(this.handleError);
    }
}