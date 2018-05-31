import { Component, Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IGolfClub } from './model';
import { environment } from '../environments/environment';

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
    this.httpClient.get<IGolfClub[]>(`${environment.apiBaseUrl}/clubs`).subscribe(response => {
      this.golfClubs = response;
    });
  }
}
