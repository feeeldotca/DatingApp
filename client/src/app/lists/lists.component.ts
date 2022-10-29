import { Component, OnInit } from '@angular/core';
import {ServiceService} from '../service.service'

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {

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
