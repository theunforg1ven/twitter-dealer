import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.scss']
})
export class PieChartComponent implements OnInit {
  @Input() inputData: any;
  private res: any;

  constructor() { }

  pieChartData: number[] = [350, 200, 420];
  pieChartLabels: string[] = ['aaa', 'bbb', 'ccc'];
  pieChartType = 'pie';

  ngOnInit(): void {
    if (this.inputData !== null && this.inputData !== undefined){
     this.parseChartData(this.inputData);
    }
  }

  public parseChartData(res: any): void {
    const data = this.res.map(o => o.total);
    const p = [];

    const chartData = data.reduce((r, e) => {
      const key = e[0];
      if (!p[key]) {
        p[key] = e;
        r.push(p[key]);
      } else {
        p[key][1] += e[1];
      }
      return r;
    }, []);
  }
}
