﻿import React from 'react';
import '../EventPost/EventPost.css';
import DayPicker from 'react-day-picker/DayPicker';
import CategoriesWidget from '../Widgets/CategoriesWidget';
import SearchWidget from '../Widgets/SearchWidget';
import { Modal } from 'react-bootstrap';
import { getUserCalendarDays, getEventsInDays } from '../../api/AttendanceApi';
import { Link } from 'react-router-dom';
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
   async componentDidMount() {

       const days = await getUserCalendarDays();
       const JsDays = days.map((item, key) => {
           return new Date(Date.parse(item));
       })
        this.setState({
            selectedDays: JsDays
        });
    }
    closeModal() {
        this.setState({ showModal: false });
    }
    async onDayClick(day, mod, e) {
        const events = await getEventsInDays(day);
        const eventsInDay = events.map((item, key) => {
            return {
                eventName: item.eventName,
                eventUrl: item.eventSlug
            }
        })
        let formattedDate = day.getDate() + "-" + (day.getMonth() + 1) + "-" + day.getFullYear();
        this.setState({
            modalDate: formattedDate,
            modalEvents: eventsInDay
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
                                <Link className="btn btn-sm btn-info mx-2" to={`/wydarzenie/szczegoly/${element.eventUrl}`}>Przejdź</Link>
                                <b>{element.eventName}</b>
                              
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
