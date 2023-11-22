import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  routes: any;

  constructor(private route: ActivatedRoute) {
    this.routes = this.route.routeConfig?.children;
  }

  ngOnInit(): void {
    
  }
  title = 'librarysystem';
}
