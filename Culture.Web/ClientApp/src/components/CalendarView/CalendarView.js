import React from 'react';
import '../EventPost/EventPost.css';
import DayPicker from 'react-day-picker/DayPicker';
import CategoriesWidget from '../Widgets/CategoriesWidget';
import SearchWidget from '../Widgets/SearchWidget';
import { Modal } from 'react-bootstrap';
import 'react-day-picker/lib/style.css';


class CalendarView extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            selectedDays: [],
            showModal: false,
            modalDate: "",
            modalEvents:[]
        };
        this.onDayClick = this.onDayClick.bind(this);
        this.closeModal = this.closeModal.bind(this);
    }
    componentDidMount() {
        this.setState({
            selectedDays: [
                new Date(2019, 8, 13),
                new Date(2019, 8, 15),
                new Date(2019, 8, 17),
                new Date(2019, 8, 19),
                new Date(2019, 8, 25)
            ]
        });
    }
    closeModal() {
        this.setState({ showModal: false });
    }
    onDayClick(day, mod, e) {
        let formattedDate = day.getDate() + "-" + (day.getMonth() + 1) + "-" + day.getFullYear();
        this.setState({
            modalDate: formattedDate,
            modalEvents: [{
                eventName: "Event1",
                eventUrl: "http://google.com"
            },
                {
                    eventName: "Event2",
                    eventUrl: "http://google.com"
                },
                {
                    eventName: "Event3",
                    eventUrl: "http://google.com"
                }
                ]
        });
        this.setState({ showModal: true });
    }
    render() {
        const MONTHS = [
            'Styczeń',
            'Luty',
            'Marzec',
            'Kwiecień',
            'Maj',
            'Czerwiec',
            'Lipiec',
            'Sierpień',
            'Wrzesień',
            'Pazdziernik',
            'Listopad',
            'Grudzień',
        ];
        const WEEKDAYS_LONG = [
            'Niedziela',
            'Poniedziałek',
            'Wtorek',
            'Sroda',
            'Czwartek',
            'Piatek',
            'Sobota'
        ];
        const WEEKDAYS_SHORT = ['Ndz', 'Pon', 'Wt', 'Sr', 'Czw', 'Pt', 'Sb'];
        
        return (
            <div className="container">
                <span style={{ fontSize: "50px" }}>Terminarz z wydarzeniami</span>
                <Modal   show={this.state.showModal} onHide={this.closeModal}>
                    <Modal.Header >
                        <Modal.Title >Wydarzenia dnia: {this.state.modalDate}</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        {this.state.modalEvents.map((element, index) => (
                            <div className=" my-4" key={index}>
                                <a className="btn btn-sm btn-info mx-2" href={element.eventUrl}>Przejdź</a>
                                {element.eventName}
                              
                                <br/>
                            </div>
                           
                        ))}
                        </Modal.Body>
                        <Modal.Footer>
                        <button className="btn btn-secondary" onClick={this.closeModal}>
                            Zamknij
                             </button>
                        </Modal.Footer>

                </Modal>
                <div className="row">
                        <DayPicker
                            containerProps={{ style: { fontSize: "40px" } }}
                            months={MONTHS}
                            weekdaysLong={WEEKDAYS_LONG}
                            weekdaysShort={WEEKDAYS_SHORT}
                            onDayClick={this.onDayClick}
                            format='MM/dd/yyyy'
                            locale="pl"
                            firstDayOfWeek={1}
                            className="col-md-8"
                            selectedDays={this.state.selectedDays}
                            />
                    <div className="col-md-4" >
                        <div className="affix">
                            <SearchWidget />
                            <CategoriesWidget />
                        </div>

                    </div>
                </div>
            </div>
            )
    }
}
export default CalendarView;
