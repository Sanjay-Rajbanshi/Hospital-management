import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [NavBarComponent]
})
export class AppComponent {
  constructor(private readonly _http: HttpClient) {

  }

  CallToServer() {
    this._http.get("api/WeatherForecast/GetWeatherForecast").subscribe({
      next: res => {
        console.log(res);
      }, error: err => {
        console.log(err);
      }
    })
  }
}
