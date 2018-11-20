import { EdituserComponent } from './users/edituser/edituser.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UserListComponent } from './admin/user-list/user-list.component';
import { TradeListComponent } from './trades/trade-list/trade-list.component';
import { OptionListComponent } from './trades/option-list/option-list.component';
import { PerformanceComponent } from './performance/performance.component';
import { AdminComponent } from './admin/admin/admin.component';
import { AuthGuard } from './_guards/auth.guard';
import { TradeDetailResolver } from './_resolvers/trade-detail.resolver';
import { MemberTradesComponent } from './trades/member-trades/member-trades.component';



export const appRoutes: Routes = [
  { path: 'home', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'users', component: UserListComponent},
      { path: 'trades', component: TradeListComponent},
      { path: 'optionlist/:tradeid', component: OptionListComponent, resolve: {trade: TradeDetailResolver}},
      { path: 'membertrades', component: MemberTradesComponent},
      { path: 'user/edit', component: EdituserComponent},
      { path: 'dashboard', component: DashboardComponent},
      { path: 'performance', component: PerformanceComponent},
      { path: 'admin', component: AdminComponent}
    ]
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full'}
];
