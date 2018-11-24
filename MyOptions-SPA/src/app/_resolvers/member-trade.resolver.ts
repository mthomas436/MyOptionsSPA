import { catchError } from 'rxjs/operators';
import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from '@angular/core';
import { Resolve, ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { Router } from '@angular/router';
import { TradeService } from '../_services/trade.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';

@Injectable()
export class MemberTradeResolver implements Resolve<User> {
  constructor(private tradeService: TradeService, private router: Router,
              private alertify: AlertifyService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<User> {
    return this.tradeService.getMemberTrades(route.params['userid']).pipe(
      catchError(error => {
        this.alertify.error('Problem retrieving data');
        this.router.navigate(['/trades']);
        return of(null);
      })
    );
  }
}
