import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { TradeService } from 'src/app/_services/trade.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-member-trades',
  templateUrl: './member-trades.component.html',
  styleUrls: ['./member-trades.component.css']
})
export class MemberTradesComponent implements OnInit {

  users: User[];

  constructor(public tradeService: TradeService,
              private alertify: AlertifyService,
              private authService: AuthService,
              private router: Router) { }

  ngOnInit() {
    this.getMembers();
  }

  getMemberTrades(userid: number) {
    this.router.navigate(['/membertrades/trades/' + userid]);
  }

  getMembers() {
    this.tradeService.getMembers().subscribe((users: User[]) => {
      this.users = users;
    }, error => {
      this.alertify.error(error);
    });
  }


}
