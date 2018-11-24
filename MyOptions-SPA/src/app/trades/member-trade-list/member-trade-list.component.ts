import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { Trade } from 'src/app/_models/trade';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-member-trade-list',
  templateUrl: './member-trade-list.component.html',
  styleUrls: ['./member-trade-list.component.css']
})
export class MemberTradeListComponent implements OnInit {

  user: User;
  trades: Trade[];
  constructor(private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {

    this.route.data.subscribe(data => {
      this.user = data['user'];
      console.log(this.user);
      this.trades = this.user.trades;
    });

  }

}
