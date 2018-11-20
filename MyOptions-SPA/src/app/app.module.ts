import { OptionAddComponent } from './trades/option-add/option-add.component';
import { AddTradeComponent } from './trades/add-trade/add-trade.component';
import { OptionEditComponent } from './trades/option-edit/option-edit.component';
import { OptionDetailComponent } from './trades/option-detail/option-detail.component';
import { OptionListComponent } from './trades/option-list/option-list.component';
import { environment } from './../environments/environment';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { EdituserComponent } from './users/edituser/edituser.component';
import { TradeDetailResolver } from './_resolvers/trade-detail.resolver';
import { AuthGuard } from './_guards/auth.guard';
import { AdminComponent } from './admin/admin/admin.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { appRoutes } from './routes';
import { AlertifyService } from './_services/alertify.service';
import { JwtModule, JWT_OPTIONS } from '@auth0/angular-jwt';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule, AlertModule, ModalModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { TradeService } from './_services/trade.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { UserListComponent } from './admin/user-list/user-list.component';
import { PerformanceComponent } from './performance/performance.component';
import { UserService } from './_services/user.service';
import { TradeListComponent } from './trades/trade-list/trade-list.component';
import { MemberTradesComponent } from './trades/member-trades/member-trades.component';
import { DatePipe, LocationStrategy, HashLocationStrategy } from '@angular/common';

export function tokenGetter() {
  return localStorage.getItem('token');
}

export function jwtOptionsFactory() {
  return {
    tokenGetter: () => {
      return tokenGetter();
    },
    whitelistedDomains: environment.whitelist,
    blacklistedRoutes: environment.blacklist
  };
}

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      TradeListComponent,
      UserListComponent,
      DashboardComponent,
      AdminComponent,
      PerformanceComponent,
      EdituserComponent,
      MemberTradesComponent,
      OptionListComponent,
      OptionDetailComponent,
      OptionEditComponent,
      AddTradeComponent,
      OptionAddComponent

   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      BsDropdownModule.forRoot(),
      RouterModule.forRoot(appRoutes, {onSameUrlNavigation: 'reload'}),
      AlertModule.forRoot(),
      BsDatepickerModule.forRoot(),
      ModalModule.forRoot(),
      JwtModule.forRoot({
        jwtOptionsProvider: {
          provide: JWT_OPTIONS,
          useFactory: jwtOptionsFactory
        }
      })
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard,
      TradeService,
      UserService,
      TradeDetailResolver,
      UserEditResolver,
      DatePipe,
      {provide: LocationStrategy, useClass: HashLocationStrategy}
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
