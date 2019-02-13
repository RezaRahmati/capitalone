import { Component, OnInit, Input } from '@angular/core';
import { TrustedScriptString } from '@angular/core/src/sanitization/bypass';

@Component({
  selector: 'app-content-item',
  templateUrl: './content-item.component.html',
  styleUrls: ['./content-item.component.scss']
})
export class ContentItemComponent implements OnInit {

  @Input() rightAlign: boolean = false;
  @Input() topic: string;

  constructor() { }

  ngOnInit() {
  }

}
