import { Component, OnInit, TemplateRef } from '@angular/core';
import { Option } from 'src/app/_models/option';
import { Trade } from 'src/app/_models/trade';
import { TradeService } from 'src/app/_services/trade.service';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';



@Component({
  selector: 'app-option-list',
  templateUrl: './option-list.component.html',
  styleUrls: ['./option-list.component.css']
})
export class OptionListComponent implements OnInit {
  tradeid: number;
  trade: Trade;
  showOptionList: boolean;
  filteredOptions: Option[];
  showOptionDetail: boolean;
  showOptionEdit: boolean;
  showOptionAddForm: boolean;
  optionSelected: boolean;
  selectedOption: Option;
  newOption: Option;
  constructor( private tradeService: TradeService,
    private authService: AuthService,
    private alertify: AlertifyService,
    private route: ActivatedRoute,
    private router: Router
    ) { }

  ngOnInit() {
    this.router.onSameUrlNavigation = 'reload';
    this.showOptionList = true;
    this.showOptionDetail = false;
    this.showOptionEdit = false;
    this.showOptionAddForm = false;
    this.newOption = null;

    this.route.data.subscribe(data => {
      this.trade = data['trade'];
    });
 
    this.filteredOptions = this.trade.options;

  }

  setSelectedOption(option: Option, type: string) {
    this.optionSelected = true;
    this.selectedOption = option;

    if (type === 'detail') {
      this.displayOptionDetail();
    }
    if (type === 'edit') {
      this.displayOptionEdit();
    }
  }

  editOption(optionid: number) {
    this.router.navigate(['/editoption/' + optionid]);
  }

  displayOptionDetail() {
    this.showOptionList = false;
    this.showOptionDetail = true;
    this.showOptionAddForm = false;
    this.showOptionEdit = false;
  }

  displayOptionEdit() {
    this.showOptionList = false;
    this.showOptionDetail = false;
    this.showOptionEdit = true;
    this.showOptionAddForm = false;
  }

  displayOptionAdd() {
    this.showOptionList = false;
    this.showOptionDetail = false;
    this.showOptionEdit = false;
    this.showOptionAddForm = true;
  }

  displayOptionListWindow() {
    this.showOptionList = true;
    this.showOptionDetail = false;
    this.showOptionEdit = false;
    this.showOptionAddForm = false;
  }

  closeAddForm(reload: Boolean) {
    if (reload) {
      this.reloadTrade();
    }  else {
      this.displayOptionListWindow();
    }
  }


  reloadTrade() {
    this.tradeService.getTrade(this.trade.tradeid).subscribe((updatedtrade: Trade) => {

      this.filteredOptions.length = 0;
      updatedtrade.options.forEach(element => {
        this.filteredOptions.push(element);
      });

      this.displayOptionListWindow();
    }, error => {
      this.alertify.error(error);
    });

    // window.location.reload();
  }

  confirmDelete(optionid: number) {

    this.alertify.confirm('Do you really want to delete this option?', () => {

      this.tradeService.deletePosition(optionid).subscribe((trade: Trade) => {
        this.alertify.success('Option deleted successfully');
        this.trade = trade;
        this.tradeid = trade.tradeid;
        this.filteredOptions = trade.options;
      }, error => {
        this.alertify.error(error);
      });
    });
  }



  displayRows(status: string, event) {
    if (status === 'open') {
      this.filteredOptions = this.trade.options.filter(items => !items.positionClosed);
    } else if (status === 'closed') {
      this.filteredOptions = this.trade.options.filter(items => items.positionClosed);
    } else {
      this.filteredOptions = this.trade.options;
    }
  }

}
