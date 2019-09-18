import React from 'react';
import CommReactionBar from '../CommReactionBar/CommReactionBar';


class EventDetailsView extends React.Component {


    constructor(props) {
        super(props);

        this.state = {
        };


    }


    render() {
        return (

            <div className="container">

                <h1 className="text-center">Szczegóły wydarzenia</h1>

                <div className="row " >
                    <div className="col-md-8 h-100 " >
                        <img class="img-fluid pull-right" src="http://placehold.it/730x615" />
                        
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
                                     </div>
                                    <div className="card-body">
                                        <b>Progress Days - warsztaty z certyfikatem </b>
                                    </div>
                                </div>
                                <div className="card mb-3">
                                    <div className="card-header ">
                                        Data odbycia
                                     </div>
                                    <div className="card-body">
                                        <b>10.09.2019</b>  godz. 17:00
                                    </div>
                                </div>
                                <div className="card mb-3">
                                    <div className="card-header ">
                                        Adres
                                </div>
                                    <div className="card-body">
                                        <b> Wrocław, ul. Fabryczna 29-31</b>
                                    </div>
                                </div>
                                <div className="card mb-3">
                                    <div className="card-header ">
                                        Kategoria
                                </div>
                                    <div className="card-body">
                                        <b> Muzyka</b>
                                    </div>
                                </div>
                                <div className="card mb-3">
                                    <div className="card-header ">
                                        Cena
                                </div>
                                    <div className="card-body">
                                        <b> 12zł</b>
                                    </div>
                                </div>
                               
                            </div>
                            <div className="card-body mb-0">
                                <div className="row mb-3 ">
                                    <div className="mx-auto">
                                        <a href="#" className="btn btn-danger">Dodaj do kalendarza</a>
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="mx-auto ">
                                        <a href="#" className="btn btn-primary">Zapisz się </a>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    
                </div>

                <div className="row ">
                    <div className="col-md-8 mt-4">
                        <div className="card">
                            <div className="card-body">
                                Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis aliquid atque, nulla? Quos cum ex quis soluta, a laboriosam. Dicta expedita corporis animi vero voluptate voluptatibus possimus, veniam magni quis!
                    Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis aliquid atque, nulla? Quos cum ex quis soluta, a laboriosam. Dicta expedita corporis animi vero voluptate voluptatibus possimus, veniam magni quis!
                    Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis aliquid atque, nulla? Quos cum ex quis soluta, a laboriosam. Dicta expedita corporis animi vero voluptate voluptatibus possimus, veniam magni quis!
                    Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis aliquid atque, nulla? Quos cum ex quis soluta, a laboriosam. Dicta expedita corporis animi vero voluptate voluptatibus possimus, veniam magni quis!
                    Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis aliquid atque, nulla? Quos cum ex quis soluta, a laboriosam. Dicta expedita corporis animi vero voluptate voluptatibus possimus, veniam magni quis!
                             </div>
                            <CommReactionBar/>
                        </div>
                        
                    </div>
                    <div className="col-md-4 mt-4">
                        <div className="card ">
                            <div className="card-header text-center">
                                <div className="text-center mb-3">
                                    <b>Podobne wydarzenia</b>
                                </div>
                                <div className="card mb-3">
                                    <img src="http://placehold.it/200x100" className="card-img-top" />
                                    <div className="card-header ">
                                       <b> Progress Days - warsztaty z certyfikatem</b>
                                    </div>
                                </div>
                                <div className="card mb-3">
                                    <img src="http://placehold.it/200x100" className="card-img-top" />
                                    <div className="card-header ">
                                        <b> Progress Days - warsztaty z certyfikatem</b>
                                    </div>
                                </div>
                                <div className="card mb-3">
                                    <img src="http://placehold.it/200x100" className="card-img-top" />
                                    <div className="card-header ">
                                        <b> Progress Days - warsztaty z certyfikatem</b>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
export default EventDetailsView;
