import { Component, Input, OnInit } from '@angular/core';

const SAMPLE_BARCHART_DATA: any[] = [
  { data: [65, 59, 80, 81, 56, 54, 30], label: 'Current date'},
  { data: [25, 39, 60, 91, 36, 54, 50], label: 'Actual date'}
];

const SAMPLE_BARCHART_LABELS: string[] = ['@ester', '@shef', '@rtf', '@maidan', '@jj14', 'fuser', 'ggfromh'];

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.scss']
})
export class BarChartComponent implements OnInit {
  @Input() inputData: any;

  orders: any;
  orderLabels: string[];
  orderData: number[];

  public barChartData: any[] = SAMPLE_BARCHART_DATA;
  public barChartLabels: string[] = SAMPLE_BARCHART_LABELS;
  public barChartType = 'bar';
  public barChartLegend = true;
  public barChartOptions: any = {
    scaleShowVerticalLines: false,
    responsive: true
  };

  constructor() { }

  ngOnInit(): void {
    if (this.inputData !== null){
      this.parseChartData(this.inputData);
    }
  }

  public parseChartData(res: any): void {
    const allData = res.slice(0);
    console.log(res);
    const p = [];

    const chartData = allData.reduce((r, e) => {
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
