import { Component, OnInit } from '@angular/core';
import {ServiceService} from '../..//service.service'
@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  constructor(private service: ServiceService) { }
  users: any=[];
  ngOnInit(): void {
    this.service.getUsers().subscribe(response =>{
      this.users = response;
      if(this.users){
        console.log(response);        
      }
    }, error=>console.log(error)
    )
  }

}
