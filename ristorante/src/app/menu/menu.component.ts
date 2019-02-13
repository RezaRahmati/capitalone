import { Component, OnInit } from '@angular/core';
import { MenuService } from '../menu.service';
import { Observable } from 'rxjs';
import { MenuItem } from '../menu-item';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
  menu$: Observable<Array<MenuItem>>;

  constructor(private menuService: MenuService) { }

  ngOnInit() {
    this.menu$ = this.menuService.getMenu();
  }

}
