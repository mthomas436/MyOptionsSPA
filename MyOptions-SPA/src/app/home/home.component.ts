import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = true;
  userid: number;
  loggedin: boolean;
  constructor(public authService: AuthService) { }

  ngOnInit() {
    this.loggedin = this.authService.loggedIn();
    if ( !this.loggedin ) {
      this.registerMode = true;
    }
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }
}
