import React from 'react';
import CommReactionBar from '../CommReactionBar/CommReactionBar';
import { getEventDetails, getParticipants, editEvent } from '../../api/EventApi';
import { addToCalendar, removeFromCalendar, signUser, unsignUser } from '../../api/AttendanceApi';
import { userIsAuthenticated, getUserId, isAdmin } from '../../utils/JwtUtils';
import '../EventDetailsView/EventDetailsView.css';
import RecommendedEvent from '../RecommendedEvent/RecommendedEvent';
import { Redirect, Link} from 'react-router-dom';
import { Modal } from 'react-bootstrap';
import SearchWidget from '../Widgets/SearchWidget';
import MyMap from '../MyMap/MyMap';
import {PDFDownloadLink} from '@react-pdf/renderer';
import EventPDF from '../EventPDF/EventPDF';



String.prototype.escapeDiacritics = function () {
    return this.replace(/ą/g, 'a').replace(/Ą/g, 'A')
        .replace(/ć/g, 'c').replace(/Ć/g, 'C')
        .replace(/ę/g, 'e').replace(/Ę/g, 'E')
        .replace(/ł/g, 'l').replace(/Ł/g, 'L')
        .replace(/ń/g, 'n').replace(/Ń/g, 'N')
        .replace(/ó/g, 'o').replace(/Ó/g, 'O')
        .replace(/ś/g, 's').replace(/Ś/g, 'S')
        .replace(/ż/g, 'z').replace(/Ż/g, 'Z')
        .replace(/ź/g, 'z').replace(/Ź/g, 'Z');
};

class EventDetailsView extends React.Component {


    constructor(props) {
        super(props);
       
        this.state = {
            name: "",
            isInCalendar: false,
            isSigned: false,
            id: null,
            price: null,
            createdBy: "",
            urlSlug: this.props.match.params.eventSlug,
            category: "",
            reactions: [],
            comments: [],
            date: null,
            imagePath: null,
            isPreview: false,
            commentsCount: null,
            reactionsCount: null,
            canLoadMore: false,
            currentReaction: null,
            cityName: "",
            streetName: "",
            takesPlaceHour: "",
            takesPlaceDate: null,
            recommendedEvents: [],
            participants: [],
            participantsNumber: null,
            showModal: false,
            query: '',
            avatarPath: null,
            pdf: null,
            authorId: null,
            type: '',
            editPrice: false,
            editDate: false,
            editName: false,
            editAddress: false,
            editCategory: false,
            done:false
        };

    }

    DisplayCalendarButton = () => {
        if (this.state.isInCalendar) return "Usuń z kalendarza";
        else {
            return "Dodaj do kalendarza";
        }
    }

    async componentDidUpdate(prevProps, prevState) {
        if (this.props.match.params.eventSlug !== prevProps.match.params.eventSlug) {
            await this.setState({ urlSlug: this.props.match.params.eventSlug})
            await this.componentDidMount();
        }

    }

    DisplayAttendButton = () => {
        if (this.state.isSigned) return "Wypisz się";
        else {
            return "Biorę udział";
        }
    }

    SignUser = async () => {

        if (this.state.isSigned) {
            await unsignUser(this.state.id);
            this.setState({
                isSigned: false
            })
        }
        else {
            await signUser(this.state.id);
            this.setState({
                isSigned: true
            });
        }
    }

    HandleCalendar = async () => {
        if (this.state.isInCalendar) {
            await removeFromCalendar(this.state.id);
            this.setState({
                isInCalendar: false
            })
        }
        else {
            await addToCalendar(this.state.id);
            this.setState({
                isInCalendar: true
            });
        }
    }

    handleOnChange = (e) => {
        this.setState({ [e.target.name]: e.target.value });

    }

    handleRecommendedEventClick = async (url) => {
        this.props.history.push(`/wydarzenie/szczegoly/${url}`);
        await this.setState({
            redirect: true,
            urlSlug: url
        });
        await this.componentDidMount();
    }


    displayRecommendedEvents = () => {

        console.log(this.state.recommendedEvents)
        return this.state.recommendedEvents.map((item, key) => {
            return <RecommendedEvent
                key={key}
                redirect={item.urlSlug}
                name={item.name}
                source={item.imageSource}
                handleRecommendedEventClick={this.handleRecommendedEventClick}
            />
        });

    }

    renderParticipants = () => {
       const items =  this.state.participants.map((item, key) => {

          return <Link to={`/konto/${item.userId}`}>
              {item.username}
              <br/>
           </Link>;
       });
        console.log(items);
        return items;
    }

    onModalClose = () => {
        this.setState({ showModal: false });
    }

    onModalMapClose = () => {
        console.log('xd')
        this.setState({ showMapModal: false });
    }

    participantsClick = async () => {
        if (this.state.participants.length === 0) {
            const result = await getParticipants(this.state.id,null);
            console.log(result);
            await this.setState({ participants: result });
        }
        this.setState({ showModal: true });
    }

    addressClick = () => {
        this.setState({ showMapModal: true });
    }

    handleSearch = async (e) => {
        e.preventDefault();
        const result = await getParticipants(this.state.id, this.state.query);

         this.setState({ participants: result });
    }

    editEventFlag = (e) => {
            this.setState({ [e.target.getAttribute('name')] : true});
    }

    editEvent = (e) => {
        e.preventDefault();
        editEvent(
            this.state.id,
            this.state.name,
            this.state.category,
            this.state.takesPlaceDate,
            this.state.takesPlaceHour,
            this.state.price,
            this.state.cityName,
            this.state.content,
            this.state.streetName
        )


        this.setState({
            editName: false,
            editCategory: false,
            editDate: false,
            editAddress: false,
            editPrice: false
        });
    }

    async componentDidMount() {
        const result = await getEventDetails(this.state.urlSlug);
        console.log(result);
        const jsDate = new Date(Date.parse(result.creationDate));
        const jsDateFormatted = jsDate.getUTCDate() + "-" + (jsDate.getMonth() + 1) + "-" + jsDate.getFullYear();

        const jsDatePlace = new Date(Date.parse(result.takesPlaceDate));
        const jsDatePlaceFormatted = jsDatePlace.getUTCDate() + "-" + (jsDatePlace.getMonth() + 1) + "-" + jsDatePlace.getFullYear();
        const jsDatePlaceTimeFormatted = jsDatePlace.getHours() + ":" + (jsDatePlace.getMinutes() < 10 ? '0' : '') + jsDatePlace.getMinutes();

        await this.setState({
            canLoadMore: result.canLoadMore,
            category: result.category,
            name: result.name,
            id: result.id,
            cityName: result.cityName,
            imagePath: result.image,
            reactions: result.reactions,
            streetName: result.streetName,
            comments: result.comments,
            commentsCount: result.commentsCount,
            reactionsCount: result.reactionsCount,
            currentReaction: result.currentReaction,
            content: result.content,
            createdBy: result.createdBy,
            date: jsDateFormatted,
            price: result.price,
            takesPlaceDate: jsDatePlaceFormatted,
            takesPlaceHour: jsDatePlaceTimeFormatted,
            isInCalendar: result.isInCalendar,
            isSigned: result.isUserSigned,
            recommendedEvents: result.recommendedEvents,
            participantsNumber: result.participantsNumber,
            avatarPath: result.authorAvatar,
            authorId: result.authorId
        });

      await  this.setState({
            pdf: <EventPDF
                content={this.state.content.escapeDiacritics()}
                name={this.state.name.escapeDiacritics()}
                category={this.state.category.escapeDiacritics()}
                cityName={this.state.cityName.escapeDiacritics()}
                streetName={this.state.streetName.escapeDiacritics()}
                imagePath={this.state.imagePath}
                price={this.state.price}
                takesPlaceDate={this.state.takesPlaceDate}
                takesPlaceHour={this.state.takesPlaceHour}
            />})


        this.setState({done:true})
    }

    handleInputChange = (evt) => {
        if (evt.target.name === 'date') {
            const array = evt.target.value.split(",");
            this.setState({
                takesPlaceDate: array[0],
                takesPlaceHour: array[1]
            });
        }
        else if (evt.target.name === 'address') {
            const array = evt.target.value.split(",");
            this.setState({
                streetName: array[0],
                cityName: array[1]
            });

        }
        else {
            this.setState({
                [evt.target.name]: evt.target.value
            });
        }
    }
    render() {
        
        
        return (

            <div className="container pt-5">
                {this.state.done ? 
                    <div>

                        {this.state.showModal ?
                            <Modal show={this.state.showModal} onHide={this.onModalClose} >
                                <Modal.Header>
                                    <SearchWidget
                                        handleSearch={this.handleSearch}
                                        handleOnChange={this.handleOnChange}
                                        query={this.state.participantsQuery} />
                                </Modal.Header>
                                <Modal.Body className="text-center">
                                    {this.renderParticipants()}
                                </Modal.Body>
                                <Modal.Footer>
                                    <button className="btn btn-secondary" onClick={this.onModalClose}>
                                        Zamknij
                             </button>
                                </Modal.Footer>

                            </Modal> :
                            null
                        }
                        {this.state.showMapModal ?
                            <Modal style={{ left: "-20%" }} show={this.state.showMapModal} onHide={this.onModalMapClose} >
                                <MyMap
                                    eventId={this.state.id}
                                />
                            </Modal> :
                            null
                        }

                        <div className="row " >
                            <div className="col-md-8 h-100 " >
                                <div className="card">
                                    <div className="card-header ">
                                        <img className="img-fluid pull-right mt-4" width="730" height="615" src={this.state.imagePath} />
                                        <div className=" mt-4">
                                            <div className="card">
                                                <div className="card-body showSpace">
                                                    {getUserId() === this.state.authorId || isAdmin() ?
                                                        <i name="editName" className="fas fa-pencil-alt fa-lg mr-2 pull-right" onClick={(e) => this.editEventFlag(e)} />
                                                        :
                                                        null
                                                    }
                                                    {this.state.content}
                                                </div>
                                                <CommReactionBar
                                                    currentReaction={this.state.currentReaction}
                                                    id={this.state.id}
                                                    createdBy={this.state.createdBy}
                                                    reactions={this.state.reactions}
                                                    reactionsCount={this.state.reactionsCount}
                                                    date={this.state.date}
                                                    comments={this.state.comments}
                                                    commentsCount={this.state.commentsCount}
                                                    currentReaction={this.state.currentReaction}
                                                    canLoadMore={this.state.canLoadMore}
                                                    isPreview={this.state.isPreview}
                                                    avatarPath={this.state.avatarPath}
                                                />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div className="col-md-4">
                                <div className="card ">
                                    <div className="card-header text-center">
                                        <div className="text-center mb-3">
                                            <b>Informacje ogólne</b>
                                        </div>
                                        <div className="card mb-3">
                                            <div className="card-header ">
                                                Nazwa
                    {getUserId() === this.state.authorId || isAdmin() ?
                                                    <i name="editName" className="fas fa-pencil-alt fa-lg mr-2 pull-right" onClick={(e) => this.editEventFlag(e)} />
                                                    :
                                                    null
                                                }
                                            </div>
                                            <div className="card-body">
                                                {this.state.editName === false ?
                                                    <b>{this.state.name}</b>
                                                    :
                                                    <form onSubmit={this.editEvent}>
                                                        <input
                                                            className="form-control  commentBox"
                                                            placeholder="Wprowadź dane..."
                                                            onChange={this.handleInputChange}
                                                            type="text"
                                                            value={this.state.name}
                                                            name="name"
                                                            autoComplete="off"
                                                            height="70px"
                                                            width="400px"

                                                        />
                                                    </form>
                                                }

                                            </div>
                                        </div>
                                        <div className="card mb-3">
                                            <div className="card-header ">
                                                Data odbycia
                                         {getUserId() === this.state.authorId || isAdmin() ?
                                                    <i name="editDate" className="fas fa-pencil-alt fa-lg mr-2 pull-right" onClick={(e) => this.editEventFlag(e)} />
                                                    :
                                                    null
                                                }
                                            </div>
                                            <div className="card-body">
                                                {this.state.editDate === false ?
                                                    <div>
                                                        Dnia <b> {this.state.takesPlaceDate}</b>, godz. {this.state.takesPlaceHour}
                                                    </div>
                                                    :
                                                    <form onSubmit={this.editEvent}>
                                                        <input
                                                            className="form-control  commentBox"
                                                            placeholder="Wprowadź dane..."
                                                            onChange={this.handleInputChange}
                                                            type="text"
                                                            value={this.state.takesPlaceDate + "," + this.state.takesPlaceHour}
                                                            name="date"
                                                            autoComplete="off"
                                                            height="70px"
                                                            width="400px"

                                                        />
                                                    </form>
                                                }
                                            </div>
                                        </div>

                                        <div className="card mb-3">
                                            <div className="card-header ">
                                                Adres
                                         {getUserId() === this.state.authorId || isAdmin() ?
                                                    <i name="editAddress" className="fas fa-pencil-alt fa-lg mr-2 pull-right" onClick={(e) => this.editEventFlag(e)} />
                                                    :
                                                    null
                                                }
                                            </div>
                                            <div className="card-body" >
                                                {this.state.editAddress === false ?
                                                    <div className="clickable" onClick={this.addressClick} >
                                                        <b> {this.state.streetName},  {this.state.cityName}</b>
                                                    </div>
                                                    :
                                                    <form onSubmit={this.editEvent}>
                                                        <input
                                                            className="form-control  commentBox"
                                                            placeholder="Wprowadź dane..."
                                                            onChange={this.handleInputChange}
                                                            type="text"
                                                            value={this.state.streetName + "," + this.state.cityName}
                                                            name="address"
                                                            autoComplete="off"
                                                            height="70px"
                                                            width="400px"

                                                        />
                                                    </form>
                                                }
                                            </div>
                                        </div>
                                        <div className="card mb-3">
                                            <div className="card-header ">
                                                Kategoria
                                         {getUserId() === this.state.authorId || isAdmin() ?
                                                    <i name="editCategory" className="fas fa-pencil-alt fa-lg mr-2 pull-right" onClick={(e) => this.editEventFlag(e)} />
                                                    :
                                                    null
                                                }
                                            </div>
                                            <div className="card-body">
                                                {this.state.editCategory === false ?
                                                    <div>
                                                        <b> {this.state.category}</b>
                                                    </div>
                                                    :
                                                    <form onSubmit={this.editEvent}>
                                                        <input
                                                            className="form-control  commentBox"
                                                            placeholder="Wprowadź dane..."
                                                            onChange={this.handleInputChange}
                                                            type="text"
                                                            value={this.state.category}
                                                            name="category"
                                                            autoComplete="off"
                                                            height="70px"
                                                            width="400px"

                                                        />
                                                    </form>
                                                }
                                            </div>
                                        </div>
                                        <div className="card mb-3">
                                            <div className="card-header ">
                                                Cena
                                         {getUserId() === this.state.authorId || isAdmin() ?
                                                    <i name="editPrice" className="fas fa-pencil-alt fa-lg mr-2 pull-right" onClick={(e) => this.editEventFlag(e)} />
                                                    :
                                                    null
                                                }
                                            </div>
                                            <div className="card-body">
                                                {this.state.editPrice === false ?
                                                    <div>
                                                        <b> {this.state.price === 0 ? 'Darmo' : this.state.price} zł</b>
                                                    </div>
                                                    :
                                                    <form onSubmit={this.editEvent}>
                                                        <input
                                                            className="form-control  commentBox"
                                                            placeholder="Wprowadź dane..."
                                                            onChange={this.handleInputChange}
                                                            type="number"
                                                            value={this.state.price}
                                                            name="price"
                                                            autoComplete="off"
                                                            height="70px"
                                                            width="400px"

                                                        />
                                                    </form>
                                                }
                                            </div>
                                        </div>
                                        <div className="card-header clickable ">
                                            <div onClick={this.participantsClick}>
                                                {`Liczba uczestniczących:${this.state.participantsNumber}`}
                                            </div>
                                        </div>
                                    </div>
                                    <div className="card-body mb-0">
                                        <div className="row mb-3 ">
                                            <div className="mx-auto">
                                                <input directory="" webkitdirectory="" ref="fileUploader" style={{ display: "none" }} type="file" />
                                                <button className="btn btn-warning">
                                                    <PDFDownloadLink className="pdfDownload" document={this.state.pdf} fileName={`${this.state.name}.pdf`}>
                                                        Pobierz plakat
                                        </PDFDownloadLink>
                                                </button>
                                            </div>
                                        </div>
                                        {userIsAuthenticated() ?
                                            <div>
                                                <div className="row mb-3 ">
                                                    <div className="mx-auto">
                                                        <button onClick={this.HandleCalendar} className="btn btn-danger">{this.DisplayCalendarButton()}</button>
                                                    </div>
                                                </div>
                                                <div className="row">
                                                    <div className="mx-auto ">
                                                        <button onClick={this.SignUser} className="btn btn-primary">{this.DisplayAttendButton()} </button>
                                                    </div>
                                                </div>
                                            </div>
                                            :
                                            null
                                        }
                                    </div>
                                </div>
                                {this.state.recommendedEvents.length > 0 ?
                                    <div className="card mt-4">
                                        <div className="card-header text-center">
                                            <div className="text-center mb-3">
                                                <b>Podobne wydarzenia</b>
                                            </div>
                                            {this.displayRecommendedEvents()}
                                        </div>
                                    </div>
                                    :
                                    null
                                }
                            </div>
                        </div>

                    </div> :
                    null

}
            </div>
        );
    }
}
export default EventDetailsView;
