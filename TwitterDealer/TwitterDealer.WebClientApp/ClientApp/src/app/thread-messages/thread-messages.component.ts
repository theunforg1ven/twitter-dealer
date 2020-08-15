import { Component, OnInit } from '@angular/core';
import { TweetsService } from '../services/tweets.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { StatusTweet } from '../models/statusTweet';
import { OrgData } from 'angular-org-chart/src/app/modules/org-chart/orgData';

@Component({
  selector: 'app-thread-messages',
  templateUrl: './thread-messages.component.html',
  styleUrls: ['./thread-messages.component.scss']
})
export class ThreadMessagesComponent implements OnInit {
  public link: string;
  public allThread: StatusTweet;
  public jsonArr: StatusTweet;
  //public orgData: OrgData = null;

  orgData: OrgData= {
    name: "Iron Man",
    type: 'CEO',
    children: [
        {
            name: "Captain America",
            type: 'VP',
            children: [
                {
                    name: "Hawkeye",
                    type: 'manager',
                    children: []
                },
                {
                    name: "Antman",
                    type: 'Manager',
                    children: []
                }
            ]
        },
        {
            name: "Black Widow",
            type: 'VP',
            children: [
                {
                    name: "Hulk",
                    type: 'manager',
                    children: [
                        {
                            name: "Spiderman",
                            type: 'Intern',
                            children: []
                        }
                    ]
                },
                {
                    name: "Thor",
                    type: 'Manager',
                    children: [
                        {
                            name: "Loki",
                            type: 'Team Lead',
                            children: []
                        }
                    ]
                }
            ]
        }
    ]
  };

  constructor(private tweetsService: TweetsService,
              private toastr: ToastrService,
              private router: Router) { }

  ngOnInit(): void {
   // this.jsonArr = null;
  }

  onGetAllTweets(link: string): void{
    this.tweetsService.getFullThread(link).subscribe((res: StatusTweet) => {
      this.allThread = null;
      this.allThread = res;
      //this.orgData = this.allThread;
      this.jsonArr = JSON.parse(JSON.stringify(this.allThread));
      console.log(this.allThread);
      
    }, error => {
      this.toastr.error(error, 'Error ocurred', { positionClass: 'toast-bottom-right' });
    }
    );
  }

  onClear(): void {
    this.link = '';
    this.allThread = null;
  }

}
