import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/mergeMap';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {

    public constructor(private router: Router,    private activatedRoute: ActivatedRoute,        private titleService: Title) {

    }

    ngOnInit() {
        this.router.events
        .filter((event) => event instanceof NavigationEnd)
        .map(() => this.activatedRoute)
        .map((route) => {
          while (route.firstChild) route = route.firstChild;
          return route;
        })
        .filter((route) => route.outlet === 'primary')
        .mergeMap((route) => route.data)   .subscribe((event) => this.titleService.setTitle('Controle de Frota'));
        // .subscribe((event) => this.titleService.setTitle('Frota - ' + event['title']));
    }
    
}
