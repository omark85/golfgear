import { Component, Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IGolfClub } from './model';

@Injectable()
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  golfClubs: IGolfClub[] = [];

  constructor(private httpClient: HttpClient) {
    
  }

  ngOnInit(): void {
    this.httpClient.get<IGolfClub[]>("http://localhost:4000/api/clubs").subscribe(response => {
      this.golfClubs = response;
    });
  }
}
